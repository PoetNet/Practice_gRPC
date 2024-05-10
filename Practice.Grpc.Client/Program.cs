using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using Metanit;

using var channel = GrpcChannel.ForAddress("https://localhost:5005/");
var client = new UserService.UserServiceClient(channel);

// GetAll
ListReply users = await client.ListUsersAsync(new Empty());
foreach (var item in users.Users)
{
    Console.WriteLine($"{item.Id}. {item.Name} - {item.Age}");
}

// Get
UserReply user = await client.GetUserAsync(new GetUserRequest { Id = 1 });
Console.WriteLine($"{user.Id}. {user.Name} - {user.Age}");

// Get with exception
try
{
    UserReply user2 = await client.GetUserAsync(new GetUserRequest { Id = 10 });
    Console.WriteLine($"{user2.Id}. {user2.Name} - {user2.Age}");
}
catch (RpcException ex)
{
    Console.WriteLine(ex.Status.Detail);
}

// Create
UserReply createdUser = await client.CreateUserAsync(new CreateUserRequest { Name = "Sam", Age = 28 });
Console.WriteLine($"{createdUser.Id}. {createdUser.Name} - {createdUser.Age}");

// Update
try
{
    UserReply updatedUser = await client.UpdateUserAsync(new UpdateUserRequest() { Id = 1, Name = "Sam", Age = 28 });
}
catch (RpcException ex)
{
    Console.WriteLine(ex.Status.Detail);
}

// Delete
try
{
    UserReply deletedUser = await client.DeleteUserAsync(new DeleteUserRequest() { Id = 1 });
    Console.WriteLine($"{deletedUser.Id}. {deletedUser.Name} - {deletedUser.Age}");

}
catch (RpcException ex)
{
    Console.WriteLine(ex.Status.Detail);
}