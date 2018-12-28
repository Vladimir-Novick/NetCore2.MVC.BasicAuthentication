# NetCore2.MVC.BasicAuthentication
Simple NetCore2 MVC Web Application with basic authentication

## Basic authentication scheme

The "Basic" HTTP authentication scheme is defined in RFC 7617, which transmits credentials as user ID/password pairs, encoded using base64.

Basic authentication is a simple authentication scheme built into the HTTP protocol. The client sends HTTP requests with the Authorization header that contains the word Basic word followed by a space and a base64-encoded string username:password .

## CSharp example:

       HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(sendParam.address);
       request.ContinueTimeout = 88000;
       request.ContentType = "application/json; charset=utf-8";
       request.Accept = "application/json, text/javascript, */*";
       request.Method = "POST";
       String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(UserName_Authotication + ":" + Password_Authontification));
       request.Headers.Add("Authorization", "Basic " + encoded);

	   using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
       {
          writer.Write(strRequest);
       }
       WebResponse response = request.GetResponse();
       using ( Stream stream = response.GetResponseStream()){
                    json = "";

                    using (StreamReader reader = new StreamReader(stream))
                    {
                        while (!reader.EndOfStream)
                        {
                            json += reader.ReadLine();
                        }
                    }
		}
		
## With Tls12 Security protocol	

      using System;
      using System.IO;
      using System.Net;

      namespace Crawler
     {
         public class WebReader
        {

           private CredentialCache GetCredential(string url,string WebUserName,string WebPassword)
          {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            CredentialCache credentialCache = new CredentialCache();
            credentialCache.Add(new System.Uri(url), "Basic", new NetworkCredential(WebUserName,
                 WebPassword));
            return credentialCache;
          }

          public string SendHttpReq(string sUrl,string WebUserName,string WebPassword)
         {
            var encoding = System.Text.Encoding.UTF8;

            string lcHtml = null;

            try
            {

                // *** Set properties
                var loHttp = (HttpWebRequest)WebRequest.Create(sUrl);

                loHttp.Timeout = 5350000;

                loHttp.Credentials = GetCredential(sUrl,WebUserName,WebPassword);
                loHttp.PreAuthenticate = true;

                    // *** Retrieve request info headers
                    using (var loWebResponse = (HttpWebResponse)loHttp.GetResponse())
                    {
                        using (var responseStream = loWebResponse.GetResponseStream())
                        {

                            using (var loResponseStream = new StreamReader(loWebResponse.GetResponseStream(), encoding))
                            {

                                lcHtml = loResponseStream.ReadToEnd();
                                loResponseStream.Close();

                            }
                        }
                    }
            } catch ( Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return lcHtml;

          }
       }
    }

		
## JavaScript Example

		var UserName="myUserName";
		var password="myPassword";

		$.ajax({
			type: 'POST',
			url: 'myLink',
			headers: {
				"Authorization": "Basic " + btoa(UserName+":"+password);
			},
			success : function(data) {
				//Success block  
			},
			error: function (xhr,ajaxOptions,throwError){
				//Error block 
			},
		 });	
		 
		 
		
Copyright (C) 2017-2018 by Vladimir Novick http://www.linkedin.com/in/vladimirnovick , 

vlad.novick@gmail.com , http://www.sgcombo.com , https://github.com/Vladimir-Novick
		 
# License
		 
		 Copyright (C) 2016-2018 by Vladimir Novick http://www.linkedin.com/in/vladimirnovick

		Permission is hereby granted, free of charge, to any person obtaining a copy
		of this software and associated documentation files (the "Software"), to deal
		in the Software without restriction, including without limitation the rights
		to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
		copies of the Software, and to permit persons to whom the Software is
		furnished to do so, subject to the following conditions:

		The above copyright notice and this permission notice shall be included in
		all copies or substantial portions of the Software.

		THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
		IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
		FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
		AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
		LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
		OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
		THE SOFTWARE. 

		
