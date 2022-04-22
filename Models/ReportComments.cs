using DotNews.Data;

namespace DotNews.Models
{
    public class ReportComments
    {
        public Report Report { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
