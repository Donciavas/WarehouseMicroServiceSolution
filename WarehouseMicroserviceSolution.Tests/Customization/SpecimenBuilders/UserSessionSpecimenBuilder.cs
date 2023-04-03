using AutoFixture.Kernel;
using DataAccess.AuthModels;

namespace WarehouseMicroserviceSolution.Tests.Customization.SpecimenBuilders
{
    public class UserSessionSpecimenBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is Type type && type == typeof(UserSession))
            {
                return new UserSession { UserName = "admin", Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluIiwiUm9sZSI6IkFkbWluaXN0cmF0b3IiLCJuYmYiOjE2ODAyODQwMTYsImV4cCI6MTY4MDI4NTIxNiwiaWF0IjoxNjgwMjg0MDE2fQ.64JXiuv7HSvGom7zNRo_TKBoPHagg5861Gxsdt1GFzs", Role = "Administrator", ExpiresIn = 1199, ExpiryTimeStamp = DateTime.Now };
            }
            return new NoSpecimen();
        }
    }
}