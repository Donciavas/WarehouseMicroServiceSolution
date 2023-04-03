using AutoFixture.Kernel;
using DataAccess.AuthModels;

namespace WarehouseMicroserviceSolution.Tests.Customization.SpecimenBuilders
{
    public class UserAccountSpecimenBuilder : ISpecimenBuilder
    {
        private byte[] password = { 246, 237, 111, 175, 154, 31, 141, 189, 45, 234, 228, 93, 177, 214, 254,
                                    182, 237, 250, 152, 94, 135, 90, 192, 134, 238, 86, 131, 93, 26, 167, 47, 83, 27, 217,
                                    108, 123, 16, 37, 77, 139, 75, 226, 245, 243, 239, 23, 197, 97, 130, 171, 87, 189, 61,
                                    165, 50, 186, 153, 226, 167, 17, 16, 193, 1, 179 };
        private byte[] passwordSalt = {219, 146, 250, 130, 121, 203, 196, 246, 161, 183, 20, 229, 147, 172, 185, 11, 255, 137,
                                       101, 223, 197, 102, 160, 233, 99, 213, 241, 224, 85, 98, 219, 218, 1, 195, 146, 244, 176,
                                       238, 190, 67, 3, 248, 240, 182, 150, 164, 89, 29, 199, 102, 60, 52, 200, 90, 1, 132, 208,
                                       138, 50, 230, 153, 114, 131, 247, 121, 243, 14, 151, 70, 236, 164, 151, 128, 2, 208, 225,
                                       170, 58, 107, 54, 49, 122, 141, 81, 118, 203, 17, 113, 63, 128, 51, 206, 63, 175, 182, 24,
                                       193, 130, 197, 59, 112, 30, 126, 12, 191, 228, 196, 137, 229, 240, 203, 196, 241, 130, 29,
                                       170, 72, 9, 110, 234, 49, 69, 25, 10, 106, 167, 156, 144};
        public object Create(object request, ISpecimenContext context)
        {
            if (request is Type type && type == typeof(UserAccount))
            {
                return new UserAccount { Id = Guid.NewGuid(), UserName = "admin", Password = password, PasswordSalt = passwordSalt, Role = "Administrator" };
            }
            return new NoSpecimen();
        }
    }
}

