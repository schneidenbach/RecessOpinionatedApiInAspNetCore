using Microsoft.AspNetCore.Mvc;

namespace OpinionatedApiExample.Shared
{
    public interface IUrlHelperProvider
    {
        IUrlHelper Url { get; }
    }
}