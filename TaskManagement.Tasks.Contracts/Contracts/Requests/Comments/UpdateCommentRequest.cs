namespace TodoManagement.Todos.Contracts.Contracts.Requests.Comments
{
    public record UpdateCommentRequest(Guid TodoId, Guid CommentId, string Comment);
}
