using Domain.Entities;

namespace Application.RepositoryInterfaces
{
    public interface IEmailTemplateRepository : IGenericRepository<EmailTemplate>
    {
        EmailTemplate GetEmailTemplateByName(string name);
    }
}