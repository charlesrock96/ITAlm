// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#nullable enable

using DocumentFormat.OpenXml.Framework;
using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
    /// <summary>
    /// Defines the WorkbookRevisionHeaderPart
    /// </summary>
    [ContentType(ContentTypeConstant)]
    [RelationshipTypeAttribute(RelationshipTypeConstant)]
    [PartConstraint(typeof(WorkbookRevisionLogPart), false, true)]
    public partial class WorkbookRevisionHeaderPart : OpenXmlPart, IFixedContentTypePart
    {
        internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.spreadsheetml.revisionHeaders+xml";
        internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/revisionHeaders";
        private DocumentFormat.OpenXml.Spreadsheet.Headers? _rootElement;

        /// <summary>
        /// Creates an instance of the WorkbookRevisionHeaderPart OpenXmlType
        /// </summary>
        internal protected WorkbookRevisionHeaderPart()
        {
        }

        /// <inheritdoc/>
        public sealed override string ContentType => ContentTypeConstant;

        /// <summary>
        /// Gets or sets the root element of this part.
        /// </summary>
        public DocumentFormat.OpenXml.Spreadsheet.Headers Headers
        {
            get
            {
                if (_rootElement is null)
                {
                    LoadDomTree<DocumentFormat.OpenXml.Spreadsheet.Headers>();
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

        private protected override OpenXmlPartRootElement? InternalRootElement
        {
            get
            {
                return _rootElement;
            }

            set
            {
                _rootElement = value as DocumentFormat.OpenXml.Spreadsheet.Headers;
            }
        }

        internal override OpenXmlPartRootElement? PartRootElement => Headers;

        /// <inheritdoc/>
        public sealed override string RelationshipType => RelationshipTypeConstant;

        /// <inheritdoc/>
        internal sealed override string TargetName => "revisionHeaders";

        /// <inheritdoc/>
        internal sealed override string TargetPath => "revisions";

        /// <summary>
        /// Gets the WorkbookRevisionLogParts of the WorkbookRevisionHeaderPart
        /// </summary>
        public IEnumerable<WorkbookRevisionLogPart> WorkbookRevisionLogParts => GetPartsOfType<WorkbookRevisionLogPart>();

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
                case WorkbookRevisionLogPart.RelationshipTypeConstant:
                    return new WorkbookRevisionLogPart();
            }

            throw new ArgumentOutOfRangeException(nameof(relationshipType));
        }
    }
}
