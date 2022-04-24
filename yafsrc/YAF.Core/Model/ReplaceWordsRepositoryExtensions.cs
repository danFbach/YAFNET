﻿/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjørnar Henden
 * Copyright (C) 2006-2013 Jaben Cargman
 * Copyright (C) 2014-2022 Ingo Herbote
 * https://www.yetanotherforum.net/
 * 
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at

 * https://www.apache.org/licenses/LICENSE-2.0

 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */
namespace YAF.Core.Model;

using YAF.Core.Extensions;
using YAF.Types.Interfaces.Data;
using YAF.Types.Models;

/// <summary>
///     The replace words repository extensions.
/// </summary>
public static class ReplaceWordsRepositoryExtensions
{
    #region Public Methods and Operators

    /// <summary>
    /// Saves changes to a word.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <param name="replaceWordId">The replace word identifier.</param>
    /// <param name="badWord">The bad word.</param>
    /// <param name="goodWord">The good word.</param>
    /// <param name="boardId">The board identifier.</param>
    public static void Save(
        this IRepository<Replace_Words> repository,
        [CanBeNull] int? replaceWordId,
        [NotNull] string badWord,
        [NotNull] string goodWord,
        [CanBeNull] int? boardId = null)
    {
        CodeContracts.VerifyNotNull(repository);

        repository.Upsert(
            new Replace_Words
                {
                    BoardID = boardId ?? repository.BoardID,
                    ID = replaceWordId ?? 0,
                    BadWord = badWord,
                    GoodWord = goodWord
                });
    }

    #endregion
}