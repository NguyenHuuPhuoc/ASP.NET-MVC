namespace ASPNetIdentityManagment.IService
{
    public interface IBaseService
    {
        void CommitChanges();

        void CommitTran();

        void BeginTran();

        void RollbackTran();

        string UnsignToString(string input);

        string StandardizedCode(string inputCode);
    }
}