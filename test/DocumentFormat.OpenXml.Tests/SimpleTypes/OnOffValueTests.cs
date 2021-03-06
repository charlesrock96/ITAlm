// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace DocumentFormat.OpenXml.Tests.SimpleTypes
{
    public class OnOffValueTests : OpenXmlComparableSimpleValueTests<bool>
    {
        protected override OpenXmlComparableSimpleValue<bool> Create(bool input) => new OnOffValue(input);

        protected override bool[] Values => new[] { true, false };

        public OnOffValueTests()
        {
            SmallValue1 = new OnOffValue(false);
            SmallValue2 = new OnOffValue(false);
            LargeValue = new OnOffValue(true);
        }
    }
}
