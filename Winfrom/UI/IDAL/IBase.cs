using System;

namespace 示例
{
	// Token: 0x02000005 RID: 5
	public interface IBase<T>
	{
        // Token: 0x06000019 RID: 25
        bool Add(T user, bool isAdmin);

        // Token: 0x0600001A RID: 26
        bool Delete(int id);

		// Token: 0x0600001B RID: 27
		bool Update(T user);

		// Token: 0x0600001C RID: 28
		T GerUser(int id);
		//注册
        bool Register(T user);
	}
}
