using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Practice.Grpc.Server.Protos;

namespace Practice.Grpc.Server.Services;
public class UserApiService
{
    static int id = 0;
    static List<User> users = new()
    {
        new User(++id, "Tom", 38), 
        new User(++id, "Bob", 42)
    };

    public Task<ListReply> ListUsers(Empty request, ServerCallContext context)
    {
        var listReply = new ListReply();
        var userList = users
            .Select(item => new UserReply { Id = item.Id, Name = item.Name, Age = item.Age })
            .ToList();

        listReply.Users.AddRange(userList);
        return Task.FromResult(listReply);
    }

    public Task<UserReply> GetUser(GetUserRequest request, ServerCallContext context)
    {
        var user = users.Find(u => u.Id == request.Id);
        if (user == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
        }

        UserReply userReply = new UserReply { Id = user.Id, Name = user.Name, Age = user.Age };
        return Task.FromResult(userReply);
    }

    public Task<UserReply> CreateUser(CreateUserRequest request, ServerCallContext context)
    {
        var user = new User(++id, request.Name, request.Age);
        users.Add(user);
        var reply = new UserReply() { Id = user.Id, Name = user.Name, Age = user.Age };
        return Task.FromResult(reply);
    }

    public Task<UserReply> UpdateUser(UpdateUserRequest request, ServerCallContext context)
    {
        var user = users.Find(u => u.Id == request.Id);

        if (user == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
        }
        // обновляем даннные
        user.Name = request.Name;
        user.Age = request.Age;

        var reply = new UserReply() { Id = user.Id, Name = user.Name, Age = user.Age };
        return Task.FromResult(reply);
    }

    public Task<UserReply> DeleteUser(DeleteUserRequest request, ServerCallContext context)
    {
        var user = users.Find(u => u.Id == request.Id);

        if (user == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
        }

        users.Remove(user);
        var reply = new UserReply() { Id = user.Id, Name = user.Name, Age = user.Age };
        return Task.FromResult(reply);
    }
}
