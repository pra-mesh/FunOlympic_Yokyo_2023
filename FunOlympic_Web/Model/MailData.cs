﻿namespace FunOlympic_Web;
public class MailData
{
    // Receiver
    public string To { get; }


    // Sender
    public string? From { get; }

    public string? DisplayName { get; }

    public string? ReplyTo { get; }

    public string? ReplyToName { get; }

    // Content
    public string Subject { get; }

    public string? Body { get; }

    public MailData(string to, string subject, string? body = null, string? from = null, string? displayName = null, string? replyTo = null, string? replyToName = null)
    {
        // Receiver
        To = to;
        // Sender
        From = from;
        DisplayName = displayName;
        ReplyTo = replyTo;
        ReplyToName = replyToName;

        // Content
        Subject = subject;
        Body = body;
    }
}