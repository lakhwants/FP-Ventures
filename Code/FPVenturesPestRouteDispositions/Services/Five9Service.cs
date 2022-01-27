﻿using FPVenturesFive9PestRouteDispositions.Constants;
using FPVenturesFive9PestRouteDispositions.Models;
using FPVenturesFive9PestRouteDispositions.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace FPVenturesPestRouteDispositions.Services
{
	public class Five9Service : IFive9Service
	{
		public readonly Five9ZohoConfigurationSettings _five9ZohoConfigurationSettings;
		public Five9Service(Five9ZohoConfigurationSettings five9ZohoConfigurationSettings)
		{
			_five9ZohoConfigurationSettings = five9ZohoConfigurationSettings;
		}
		public List<Five9Model> CallWebService(DateTime startDate, DateTime endDate)
		{
			List<Five9Model> five9Models = new List<Five9Model>();


			//RunReport
			MakeRequestToFive9(string.Format(SoapRequestBodies.RunReport, "Call Log Hawx Integration", endDate.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK"), startDate.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK")), _five9ZohoConfigurationSettings.Five9BaseUrl, _five9ZohoConfigurationSettings.Five9MethodAction, out HttpWebRequest webRequest, out IAsyncResult asyncResult);
			string reportId = GetValuesFromSoapResponse<string>(webRequest, asyncResult, "runReportResponse", "http://service.admin.ws.five9.com/");

			if (string.IsNullOrEmpty(reportId))
				return null;

			//IsReportRunning
			bool isReportRunning = true;
			while (isReportRunning)
			{
				Thread.Sleep(3000);
				MakeRequestToFive9(string.Format(SoapRequestBodies.IsReportRunning, reportId), _five9ZohoConfigurationSettings.Five9BaseUrl, _five9ZohoConfigurationSettings.Five9MethodAction, out HttpWebRequest webRequestIsReportRunning, out IAsyncResult asyncResultIsReportRunning);
				isReportRunning = GetValuesFromSoapResponse<bool>(webRequestIsReportRunning, asyncResultIsReportRunning, "isReportRunningResponse", "http://service.admin.ws.five9.com/");
			}

			Thread.Sleep(5000); //Report result is ready after a delay

			//GetReportResult
			MakeRequestToFive9(string.Format(SoapRequestBodies.GetReportResultCsv, reportId), _five9ZohoConfigurationSettings.Five9BaseUrl, _five9ZohoConfigurationSettings.Five9MethodAction, out HttpWebRequest webRequestGetReportResultCsv, out IAsyncResult asyncResultGetReportResultCsv);

			string csvResult = GetValuesFromSoapResponse<string>(webRequestGetReportResultCsv, asyncResultGetReportResultCsv, "getReportResultCsvResponse", "http://service.admin.ws.five9.com/", "return");
			if (string.IsNullOrEmpty(csvResult))
				return null;

			var csvTable = ConvertCSVtoDataTable(csvResult);

			five9Models.AddRange(CsvToFive9Model(csvTable));


			return five9Models;
		}

		private static List<Five9Model> CsvToFive9Model(DataTable csvTable)
		{
			List<Five9Model> five9Models = new();

			for (int i = 0; i < csvTable.Rows.Count; i++)
			{
				Five9Model five9Model = new();
				five9Model.Timestamp = csvTable.Rows[i][0].ToString();
				five9Model.DNIS = csvTable.Rows[i][1].ToString();
				five9Model.ANI = csvTable.Rows[i][2].ToString();
				five9Model.Campaign = csvTable.Rows[i][3].ToString();
				five9Model.CallType = csvTable.Rows[i][4].ToString();
				five9Model.Disposition = csvTable.Rows[i][5].ToString();
				five9Model.CallTime = csvTable.Rows[i][6].ToString();
				five9Model.ListName = csvTable.Rows[i][7].ToString();
				five9Model.AgentName = csvTable.Rows[i][8].ToString();
				five9Model.CallID = csvTable.Rows[i][9].ToString();
				five9Models.Add(five9Model);
			}

			return five9Models;
		}

		private static void MakeRequestToFive9(string body, string _url, string _action, out HttpWebRequest webRequest, out IAsyncResult asyncResult)
		{
			XmlDocument soapEnvelopeXml = CreateSoapEnvelope(body);
			webRequest = CreateWebRequest(_url, _action);
			InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

			asyncResult = webRequest.BeginGetResponse(null, null);

			asyncResult.AsyncWaitHandle.WaitOne();
		}

		private static DataTable ConvertCSVtoDataTable(string strFilePath)
		{
			byte[] byteArray = Encoding.UTF8.GetBytes(strFilePath);
			MemoryStream stream = new(byteArray);
			StreamReader sr = new(stream);
			string[] headers = sr.ReadLine().Split(',');
			DataTable dt = new();
			foreach (string header in headers)
			{
				dt.Columns.Add(header);
			}
			while (!sr.EndOfStream)
			{
				string[] rows = Regex.Split(sr.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
				DataRow dr = dt.NewRow();
				for (int i = 0; i < headers.Length; i++)
				{
					dr[i] = rows[i];
				}
				dt.Rows.Add(dr);
			}
			return dt;
		}

		private static T GetValuesFromSoapResponse<T>(HttpWebRequest webRequest, IAsyncResult asyncResult, string descendants, XNamespace ns2, string elementValue = null)
		{
			object value = null;
			using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
			{
				using (Stream rd = webResponse.GetResponseStream())
				{
					var doc = XDocument.Load(rd);
					IEnumerable<XElement> result;
					if (elementValue == null)
					{
						result = doc.Root.Descendants(ns2 + descendants);
					}
					else
					{
						result = doc.Root.Descendants(ns2 + descendants).Elements(elementValue);
					}

					foreach (XElement element in result)
					{
						value = element.Value;
					}

				}
			}
			return (T)Convert.ChangeType(value, typeof(T)); ;
		}

		private static HttpWebRequest CreateWebRequest(string url, string action)
		{
			HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
			webRequest.Headers.Add("SOAPAction", action);
			webRequest.Headers.Add("Authorization", "Basic YW5kcmV3bGVhZHpAZ21haWwuY29tOkhhd3gkJDI0NDI=");
			webRequest.ContentType = "text/xml;charset=\"utf-8\"";
			webRequest.Method = "POST";
			return webRequest;
		}

		private static XmlDocument CreateSoapEnvelope(string body)
		{
			XmlDocument soapEnvelopeDocument = new();
			soapEnvelopeDocument.LoadXml(body);
			return soapEnvelopeDocument;
		}

		private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
		{
			using (Stream stream = webRequest.GetRequestStream())
			{
				soapEnvelopeXml.Save(stream);
			}
		}
	}
}