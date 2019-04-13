﻿// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TinyCsvParser.TypeConverter;

namespace TinyCsvParser.Test.TypeConverter
{
    public enum TestEnum
    {
        A = 1
    }

    [TestClass]
    public class EnumConverterCaseSensitiveTest : BaseConverterTest<TestEnum>
    {

        protected override ITypeConverter<TestEnum> Converter
        {
            get { return new EnumConverter<TestEnum>(false); }
        }

        protected override Tuple<string, TestEnum>[] SuccessTestData
        {
            get
            {
                return new[] {
                    MakeTuple("A", TestEnum.A),
                };
            }
        }

        protected override string[] FailTestData
        {
            get { return new[] { "B", string.Empty, "a", null }; }
        }
    }

    public class EnumConverterCaseInsensitiveTest : BaseConverterTest<TestEnum>
    {

        protected override ITypeConverter<TestEnum> Converter
        {
            get { return new EnumConverter<TestEnum>(true); }
        }

        protected override Tuple<string, TestEnum>[] SuccessTestData
        {
            get
            {
                return new[] {
                    MakeTuple("A", TestEnum.A),
                    MakeTuple("a", TestEnum.A),
                };
            }
        }

        protected override string[] FailTestData
        {
            get { return new[] { "B", " ", string.Empty, null }; }
        }
    }

    [TestClass]
    public class EnumConverterGeneralTest
    {
        private struct NoEnum 
        {
        }

        [TestMethod]
        public void CouldNotInstantiateNonEnumTest()
        {
            Assert.ThrowsException<ArgumentException>(() => new EnumConverter<NoEnum>());
        }
    }
}
