using Microsoft.AspNetCore.Mvc;

namespace OpinionatedApiExample.Shared
{
    public interface IUrlHelperContainer
    {
        IUrlHelper Url { get; }
    }
}