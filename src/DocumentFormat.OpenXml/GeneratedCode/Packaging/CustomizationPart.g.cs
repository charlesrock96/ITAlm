// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#nullable enable

using DocumentFormat.OpenXml.Framework;
using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
    /// <summary>
    /// Defines the CustomizationPart
    /// </summary>
    [ContentType(ContentTypeConstant)]
    [RelationshipTypeAttribute(RelationshipTypeConstant)]
    [PartConstraint(typeof(WordAttachedToolbarsPart), false, false)]
    public partial class CustomizationPart : OpenXmlPart, IFixedContentTypePart
    {
        internal const string ContentTypeConstant = "application/vnd.ms-word.keyMapCustomizations+xml";
        internal const string RelationshipTypeConstant = "http://schemas.microsoft.com/office/2006/relationships/keyMapCustomizations";
        private DocumentFormat.OpenXml.Office.Word.TemplateCommandGroup? _rootElement;

        /// <summary>
        /// Creates an instance of the CustomizationPart OpenXmlType
        /// </summary>
        internal protected CustomizationPart()
        {
        }

        /// <inheritdoc/>
        public sealed override string ContentType => ContentTypeConstant;

        private protected override OpenXmlPartRootElement? InternalRootElement
        {
            get
            {
                return _rootElement;
            }

            set
            {
                _rootElement = value as DocumentFormat.OpenXml.Office.Word.TemplateCommandGroup;
            }
        }

        internal override OpenXmlPartRootElement? PartRootElement => TemplateCommandGroup;

        /// <inheritdoc/>
        public sealed override string RelationshipType => RelationshipTypeConstant;

        /// <inheritdoc/>
        internal sealed override string TargetName => "customizations";

        /// <inheritdoc/>
        internal sealed override string TargetPath => ".";

        /// <summary>
        /// Gets or sets the root element of this part.
        /// </summary>
        public DocumentFormat.OpenXml.Office.Word.TemplateCommandGroup TemplateCommandGroup
        {
            get
            {
                if (_rootElement is null)
                {
                    LoadDomTree<DocumentFormat.OpenXml.Office.Word.TemplateCommandGroup>();
                }

                return _rootElement!;
            }

            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                SetDomTree(value);
            }
        }

        /// <summary>
        /// Gets the WordAttachedToolbarsPart of the CustomizationPart
        /// </summary>
        public WordAttachedToolbarsPart? WordAttachedToolbarsPart => GetSubPartOfType<WordAttachedToolbarsPart>();

        /// <inheritdoc/>
        internal sealed override OpenXmlPart CreatePartCore(string relationshipType)
        {
            ThrowIfObjectDisposed();
            if (relationshipType is null)
            {
                throw new ArgumentNullException(nameof(relationshipType));
            }

            switch (relationshipType)
            {
                case WordAttachedToolbarsPart.RelationshipTypeConstant:
                    return new WordAttachedToolbarsPart();
            }

            throw new ArgumentOutOfRangeException(nameof(relationshipType));
        }
    }
}
