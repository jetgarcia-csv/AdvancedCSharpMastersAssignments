using System.Reflection;

namespace Pets
{
    public static class PetExtensions
    {
        public static void Talk<TPet>(this TPet pet)
            where TPet : class
        {
            var talkMethodInfo = pet
                .GetType()
                .GetMethod("Talk")
                ?? throw new NotImplementedException($"Class '{pet.GetType().Name}' has not implemented Talk() method. ");

            talkMethodInfo.Invoke(pet, null);
        }

        public static void DoSpecial<TPet>(this TPet pet)
            where TPet : class
        {
            var customMethodInfo = pet
                .GetType()
                .GetMethods()
                .LastOrDefault(x => x.GetCustomAttribute<CustomActionAttribute>() is not null)
                ?? throw new InvalidOperationException($"Class '{pet.GetType().Name}' has no method with {nameof(CustomActionAttribute)} attribute. ");

            customMethodInfo.Invoke(pet, null);
        }

        public static IList<string> GetMethodNames<TPet>(this TPet pet)
            where TPet : class
        {
            var methodNames = pet
                .GetType()
                .GetMethods()
                .Where(x => x.GetCustomAttribute<CustomActionAttribute>() is not null)
                .Select(x => x.Name)
                .ToList();

            return methodNames;
        }

        public static void Invoke<TPet>(this TPet pet, string methodName)
            where TPet : class
        {
            var methodInfo = pet
                .GetType()
                .GetMethod(methodName)
                ?? throw new InvalidOperationException($"Class '{pet.GetType().Name}' has no method '{methodName}'. ");

            methodInfo.Invoke(pet, null);
        }
    }
}
