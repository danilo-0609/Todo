using FluentValidation;
using TodoManagement.Todos.Domain.Core.Rules;
using TodoManagement.Todos.Domain.Core.ValueObjects;

namespace TodoManagement.Todos.Application.Entities.Commands.Create.Todos;

internal sealed class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
{
    internal CreateTodoCommandValidator()
    {
        RuleFor(rule => rule.Name)
            .NotEmpty().WithMessage("Name is required")
            .Must(name =>
            {
                NameLengthRule businessRule = new(name);

                if (businessRule.IsBroken())
                {
                    return false;
                }

                return true;

            }).WithMessage("The task name must be shorter than 120 words");

        RuleFor(rule => rule.Description)
            .NotEmpty().WithMessage("Description is required")
            .Must(description =>
            {
                DescriptionLengthRule descriptionLengthRule = new(description);

                if (descriptionLengthRule.IsBroken())
                {
                    return false;
                }

                return true;

            }).WithMessage("The description must be shorter than 4000 words");

        RuleFor(rule => rule.RecurringTodo)
            .Must(value =>
            {
                RecurringTodoMustBeValidRule rule = new(value);

                if (rule.IsBroken())
                {
                    return false;
                }

                return true;

            }).WithMessage("The recurring task must be a valid value, whether it is daily, weekly or montly");

        RuleFor(rule => rule.Tags)
            .Must(values =>
            {
                var tags = new List<TodoTag>();

                values.ForEach(value =>
                {
                    var tag = TodoTag.Create(value);
                    tags.Add(tag);
                });

                TagsLengthRule tagsLengthRule = new(tags);

                if (tagsLengthRule.IsBroken())
                {
                    return false;
                }

                return true;

            }).WithMessage("The tag length must be greater than 3 words and shorter than 30 words");
    }
}
