﻿using FluentAssertions;
using RestFulTests.Fakers;
using RestFulTests.Models;
using System.Collections.Generic;
using System.Linq;
using AspNetCore.IQueryable.Extensions.Pagination;
using Xunit;

namespace RestFulTests
{
    public class PagingTests
    {
        private readonly List<User> _users;
        public PagingTests()
        {
            _users = UserFaker.GenerateUserViewModel().Generate(50);
        }

        [Fact]
        public void ShouldPaging()
        {
            var sortingByFieldName = _users.AsQueryable().Paginate(5, 0);
            sortingByFieldName.Should().HaveCount(5);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        [InlineData(-166)]
        public void ShouldGetDefaultPagingWhenLimitIsZeroOrNegative(int limit)
        {
            var sortingByFieldName = _users.AsQueryable().Paginate(limit, 0);
            sortingByFieldName.Should().HaveCount(10);
        }

        [Fact]
        public void ShouldPagingFromInterfaceImplementation()
        {
            var paginate = new UserSearch()
            {
                Limit = 5,
                Offset = 0
            };
            var sortingByFieldName = _users.AsQueryable().Paginate(paginate);
            sortingByFieldName.Should().HaveCount(5);
        }


        [Fact]
        public void ShouldLimitNotBiggerThanAtrributeMax()
        {
            var paginate = new PagingMax()
            {
                Limit = 20,
                Offset = 0
            };
            var sortingByFieldName = _users.AsQueryable().Paginate(paginate);
            sortingByFieldName.Should().HaveCount(5);
        }
    }
}
