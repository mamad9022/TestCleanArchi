using System;

namespace TestCleanArch.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
            : base($"مورد نظر یافت نشد")
        {
        }
    }
}