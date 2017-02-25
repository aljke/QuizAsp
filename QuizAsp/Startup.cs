using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuizAsp.Startup))]
namespace QuizAsp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
