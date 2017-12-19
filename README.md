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
		
## JavaScript

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