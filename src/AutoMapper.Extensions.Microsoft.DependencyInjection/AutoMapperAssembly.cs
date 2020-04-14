namespace AutoMapper
{
    using System.Reflection;

    internal class AutoMapperAssembly
    {
        public readonly Assembly Assembly;

        public AutoMapperAssembly(Assembly assembly) 
        {
            Assembly = assembly;
        }
    }
}
