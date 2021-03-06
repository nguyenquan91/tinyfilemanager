﻿using System;

namespace Rx.Data
{
    /// <summary>
    /// Represent a empty page (reused)
    /// </summary>
    internal class EmptyPage : BasePage
    {
        /// <summary>
        /// Page type = Empty
        /// </summary>
        public override PageType PageType { get { return PageType.Empty; } }

        public EmptyPage(uint pageID)
            : base(pageID)
        {
            this.ItemCount = 0;
            this.FreeBytes = PAGE_AVAILABLE_BYTES;
        }

        public EmptyPage(BasePage page)
            : this(page.PageID)
        {
            // if page is not dirty but it´s changing to empty, lets copy disk content to add in journal
            if (!page.IsDirty && page.DiskData.Length > 0)
            {
                this.DiskData = new byte[BasePage.PAGE_SIZE];
                Buffer.BlockCopy(page.DiskData, 0, this.DiskData, 0, BasePage.PAGE_SIZE);
            }
        }

        /// <summary>
        /// Update freebytes + items count
        /// </summary>
        public override void UpdateItemCount()
        {
            this.ItemCount = 0;
            this.FreeBytes = PAGE_AVAILABLE_BYTES;
        }

        #region Read/Write pages

        protected override void ReadContent(ByteReader reader)
        {
        }

        protected override void WriteContent(ByteWriter writer)
        {
        }

        #endregion Read/Write pages
    }
}