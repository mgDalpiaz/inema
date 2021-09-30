using App.Services.AutoMapper;
using AutoMapper;
using CC.IoC.BootStrapper;

namespace App.Services.Configuration
{
    public class AutoMapper
    {
        static AutoMapper()
        {
            Injector.GetServices().AddAutoMapper(c => c.AddProfile<CommandToDomainMappingProfile>(), typeof(AutoMapper));
            Injector.GetServices().AddAutoMapper(c => c.AddProfile<ExternalApiToDomainMapping>(), typeof(AutoMapper));
        }

    }
}
