﻿/**
* author:xishuai
* address:https://www.github.com/yuezhongxin/MessageManager
**/

using MessageManager.Domain.ValueObject;
using MessageManager.Infrastructure;
using System;

namespace MessageManager.Domain.Entity
{
    public class Message : IAggregateRoot
    {
        public Message(string title, string content, User sendUser)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException("title can't be null");
            }
            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentException("content can't be null");
            }
            if (sendUser == null)
            {
                throw new ArgumentException("sendUser can't be null");
            }
            this.ID = Guid.NewGuid().ToString();
            this.Title = title;
            this.Content = content;
            this.SendTime = DateTime.Now;
            this.State = MessageState.NoRead;
            this.SendUser = sendUser;
        }
        public string ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime SendTime { get; set; }
        public MessageState State { get; set; }
        public virtual User SendUser { get; set; }
        public virtual User ReceiveUser { get; set; }

        public OperationResponse Send(User receiveUser)
        {
            if (receiveUser == null)
            {
                return OperationResponse.Error("收件人规则验证失败");
            }
            this.ReceiveUser = receiveUser;
            return OperationResponse.Success("发送消息成功");
            ///to do...
        }

        //public Message Read(User readUser)
        //{
        //    if (readUser.Equals(this.ReceiveUser) && this.State == MessageState.NoRead)
        //    {
        //        this.State = MessageState.Read;
        //    }
        //    return this;
        //}
    }
}