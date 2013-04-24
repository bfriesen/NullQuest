namespace NullQuest.DependencyInjection
{
    public interface IjectContainer
    {
        T Get<T>()
            where T : class;
    }

//    public interface IJectContainer
//    {
//        T Get<T>()
//            where T : class;

//        void RegisterTransient<TAbstract, TConcrete>()
//            where TConcrete : TAbstract;

//        void RegisterSingleton<TAbstract, TConcrete>()
//            where TConcrete : TAbstract;
//    }
}
