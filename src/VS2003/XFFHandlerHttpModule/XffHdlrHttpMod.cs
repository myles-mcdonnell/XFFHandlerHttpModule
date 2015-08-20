using System;
using System.Web;

namespace XFFHandlerHttpModule
{
	public class XffHdlrHttpMod : IHttpModule
	{
		public String ModuleName
		{
			get { return "XFF Handler HTTP Module"; }
		}

		// In the Init function, register for HttpApplication 
		// events by adding your handlers.
		public void Init(HttpApplication application)
		{
			application.BeginRequest+=new EventHandler(Application_BeginRequest);
		}

		private void Application_BeginRequest(Object source,
			EventArgs e)
		{
			HttpApplication application = (HttpApplication)source;
			HttpContext context = application.Context;

			string forwardedFor = context.Request.Headers["X-Forwarded-For"];

			if (forwardedFor!=null && forwardedFor.Length>0)
			{
				//This will also change Request.UserHostAddress: http://stackoverflow.com/questions/13994582/what-is-the-difference-between-request-userhostaddress-and-request-servervariabl
				context.Request.ServerVariables["REMOTE_ADDR"] = forwardedFor.Split(",".ToCharArray())[0];
			}
		}

		public void Dispose() { }
	} 
}

