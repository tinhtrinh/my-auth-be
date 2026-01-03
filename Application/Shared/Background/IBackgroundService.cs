using System.Linq.Expressions;

namespace Application.Shared.Background;

public interface IBackgroundService
{
    void Enqueue<T>(Expression<Func<T, Task>> methodCall);
    // truyền vào 1 T để có thể lấy DI dễ hơn theo cách .net core hỗ trợ cho class,
    // thay vì truyền vào hàm thuần phải tự tạo DI scope khó hơn và tạm thời chưa tìm ra
    // => chấp nhận có thể tạo những class dùng chỉ một lần cho một chỗ
}
