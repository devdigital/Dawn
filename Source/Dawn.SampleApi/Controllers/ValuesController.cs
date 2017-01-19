namespace Dawn.SampleApi.Controllers
{
    using System;
    using System.Web.Http;

    public class ValuesController : ApiController
    {
        private readonly IIdentityService identityService;

        public ValuesController(IIdentityService identityService)
        {
            if (identityService == null)
            {
                throw new ArgumentNullException(nameof(identityService));
            }

            this.identityService = identityService;
        }

        [HttpGet]
        [Route("api/values")]
        public IHttpActionResult GetValues()
        {
            if (this.identityService.UserId == "default")
            {
                return this.Ok(new[] { 1, 2, 3 });
            }

            return this.Ok("Test");
        }
    }
}