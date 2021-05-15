using CompanyNameProjectName.Pages.Dtos;

namespace CompanyNameProjectName.Audits.Dtos
{
    public class QueryAuditLogInput : CustomeRequestDto
    {
        public string UserName { get; set; }

        public int HttpStatusCode { get; set; }

        public string HttpMethod { get; set; }

        public string ExecutionTime { get; set; }
    }
}
