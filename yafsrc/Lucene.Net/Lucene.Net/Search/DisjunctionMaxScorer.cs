﻿using System;
using System.Runtime.CompilerServices;

namespace YAF.Lucene.Net.Search
{
    /*
     * Licensed to the Apache Software Foundation (ASF) under one or more
     * contributor license agreements.  See the NOTICE file distributed with
     * this work for additional information regarding copyright ownership.
     * The ASF licenses this file to You under the Apache License, Version 2.0
     * (the "License"); you may not use this file except in compliance with
     * the License.  You may obtain a copy of the License at
     *
     *     http://www.apache.org/licenses/LICENSE-2.0
     *
     * Unless required by applicable law or agreed to in writing, software
     * distributed under the License is distributed on an "AS IS" BASIS,
     * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
     * See the License for the specific language governing permissions and
     * limitations under the License.
     */

    /// <summary>
    /// The <see cref="Scorer"/> for <see cref="DisjunctionMaxQuery"/>.  The union of all documents generated by the the subquery scorers
    /// is generated in document number order.  The score for each document is the maximum of the scores computed
    /// by the subquery scorers that generate that document, plus <see cref="tieBreakerMultiplier"/> times the sum of the scores
    /// for the other subqueries that generate the document.
    /// </summary>
    internal class DisjunctionMaxScorer : DisjunctionScorer
    {
        /// <summary>Multiplier applied to non-maximum-scoring subqueries for a document as they are summed into the result.</summary>
        private readonly float tieBreakerMultiplier;
        private int freq = -1;

        /// <summary>Used when scoring currently matching doc.</summary>
        private float scoreSum;
        private float scoreMax;

        /// <summary>
        /// Creates a new instance of <see cref="DisjunctionMaxScorer"/>
        /// </summary>
        /// <param name="weight">
        ///          The <see cref="Weight"/> to be used. </param>
        /// <param name="tieBreakerMultiplier">
        ///          Multiplier applied to non-maximum-scoring subqueries for a
        ///          document as they are summed into the result. </param>
        /// <param name="subScorers">
        ///          The sub scorers this <see cref="Scorer"/> should iterate on </param>
        public DisjunctionMaxScorer(Weight weight, float tieBreakerMultiplier, Scorer[] subScorers)
            : base(weight, subScorers)
        {
            this.tieBreakerMultiplier = tieBreakerMultiplier;
        }

        /// <summary>
        /// Determine the current document score.  Initially invalid, until <see cref="DocIdSetIterator.NextDoc()"/> is called the first time. </summary>
        /// <returns> The score of the current generated document </returns>
        public override float GetScore()
        {
            return scoreMax + (scoreSum - scoreMax) * tieBreakerMultiplier;
        }

        protected override void AfterNext()
        {
            m_doc = m_subScorers[0].DocID;
            if (m_doc != NO_MORE_DOCS)
            {
                scoreSum = scoreMax = m_subScorers[0].GetScore();
                freq = 1;
                ScoreAll(1);
                ScoreAll(2);
            }
        }

        /// <summary>
        /// Recursively iterate all subScorers that generated last doc computing sum and max
        /// </summary>
        private void ScoreAll(int root)
        {
            if (root < m_numScorers && m_subScorers[root].DocID == m_doc)
            {
                // LUCENENET specific: The explicit cast to float is required here to prevent us from losing precision on x86 .NET Framework with optimizations enabled
                float sub = (float)m_subScorers[root].GetScore();
                freq++;
                scoreSum += sub;
                scoreMax = Math.Max(scoreMax, sub);
                ScoreAll((root << 1) + 1);
                ScoreAll((root << 1) + 2);
            }
        }

        public override int Freq => freq;
    }
}