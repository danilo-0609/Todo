using ErrorOr;
using TodoManagement.Todos.Domain.Common.Primitives;

namespace TodoManagement.Todos.Domain.Common.Errors
{
    public static class Errors
    {
        public static class Todos
        {
            public static Error FileLengthGreaterThanRequired(IBusinessRules rule) =>
                Error.Validation("File.Length", rule.Message);

            public static Error FileContentTypeMustBePDF(IBusinessRules rule) =>
                Error.Validation("File.ContentType", rule.Message);

            public static Error CommentLengthGreaterThanRequired(IBusinessRules rule) =>
                Error.Validation("Comment.Length", rule.Message);

            public static Error NameLengthGreaterThanRequired(IBusinessRules rule) =>
                Error.Validation("Task.Name", rule.Message);

            public static Error DescriptionLengthGreaterThanRequired(IBusinessRules rule) =>
                Error.Validation("Task.Description", rule.Message);

            public static Error RecurringTodoNotValid(IBusinessRules rule) =>
                Error.Validation("Task.RecurringTask", rule.Message);

            public static Error RecurringTodoIsNotValid =>
                Error.Validation("Task.RecurringTask", "The recurring task must be just a valid " +
                    "value, whether it is montly weekly or daily");

            public static Error NoCommentsYet =>
                Error.Validation("Comments.NoComments", "You haven't have put any comment in this todo already");

            public static Error NoArchivesYet =>
                Error.Validation("Archives.NoArchives", "You haven't have put any archive in this todo already");

            public static Error NoTodosYet =>
                Error.Validation("Todos.NoTodos", "You haven't put any todo already");
        }
    }
}
