// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Framework
{
    /// <summary>
    /// Describes a required item.
    /// </summary>
    internal sealed class RequiredValidator : VersionedValidator
    {
        public static IValidator Instance { get; } = new RequiredValidator();

        public bool IsRequired { get; set; } = true;

        protected override void ValidateVersion(ValidationContext context)
        {
            var current = context.Stack.Current;

            if (current is null)
            {
                return;
            }

            if (IsRequired && current.Value is null && current.Property is not null)
            {
                context.CreateError(
                    description: SR.Format(ValidationResources.Sch_MissRequiredAttribute, current.Property.QName.Name),
                    id: "Sch_MissRequiredAttribute",
                    errorType: ValidationErrorType.Schema);
            }
        }
    }
}
