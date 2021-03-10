# 1. Use the Word "Bus" for Event Channels

Date: 2021-02-26

## Status

Status: Accepted on 2021-02-26  


## Context

What do you call the class that contains the events used to communicate between different components and game objects. The word "Channel" could be used. However, channel is a term for a standard class in C# used in a similar way to communicate between parallel running tasks/threads.

## Decision

To avoid collision with a similar sounding C# class, use the word "Bus" for a group of related C# events that communicate between components.

## Consequences

Avoids conflicting names.
