// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#nullable enable

using DocumentFormat.OpenXml.Framework;
using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
    /// <summary>
    /// Defines the CustomXmlPart
    /// </summary>
    [RelationshipTypeAttribute(RelationshipTypeConstant)]
    [PartConstraint(typeof(CustomXmlPropertiesPart), false, false)]
    public partial class CustomXmlPart : OpenXmlPart
    {
        internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/customXml";

        /// <summary>
        /// Creates an instance of the CustomXmlPart OpenXmlType
        /// </summary>
        internal protected CustomXmlPart()
        {
        }

        /// <summary>
        /// Gets the CustomXmlPropertiesPart of the CustomXmlPart
        /// </summary>
        public CustomXmlPropertiesPart? CustomXmlPropertiesPart => GetSubPartOfType<CustomXmlPropertiesPart>();

        /// <inheritdoc/>
        public sealed override string RelationshipType => RelationshipTypeConstant;

        /// <inheritdoc/>
        internal sealed override string TargetName => "item";

        /// <inheritdoc/>
        internal sealed override string TargetPath => "../customXML";

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
                case CustomXmlPropertiesPart.RelationshipTypeConstant:
                    return new CustomXmlPropertiesPart();
            }

            throw new ArgumentOutOfRangeException(nameof(relationshipType));
        }
    }
}
