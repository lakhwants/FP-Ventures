namespace FPVenturesFive9UnbounceDisposition.Constants
{
	public class SoapRequestBodies
	{
		public static string RunReport = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ser=""http://service.admin.ws.five9.com/"">;
											<soapenv:Header/>
											<soapenv:Body>
												<ser:runReport>
													<folderName>Shared Reports</folderName>
													<reportName>{0}</reportName>
													<criteria>
														<time>
															<end>{1}</end>
															<start>{2}</start>
														</time>
													</criteria>
												</ser:runReport>
											</soapenv:Body>
										</soapenv:Envelope>";

		public static string IsReportRunning = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ser=""http://service.admin.ws.five9.com/"">;
											   <soapenv:Header/>
											   <soapenv:Body>
												  <ser:isReportRunning>
													 <identifier>{0}</identifier>
												  </ser:isReportRunning>
											   </soapenv:Body>
											</soapenv:Envelope>";

		public static string GetReportResultCsv = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ser=""http://service.admin.ws.five9.com/"">;
											   <soapenv:Header/>
											   <soapenv:Body>
												  <ser:getReportResultCsv>
													 <!--Optional:-->
													 <identifier>{0}</identifier>
												  </ser:getReportResultCsv>
											   </soapenv:Body>
											</soapenv:Envelope>";
	}
}
