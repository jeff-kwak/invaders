# 2. Make UI components raise events on an Bus

Date: 2021-02-26

## Status

Status: Accepted on 2021-02-26  


## Context

UI components in Unity have their own event system. Something has to translate the Unity UI event to a C# game event on the event bus.

## Decision

Use an event bus definition directly. There does not need to be an intermediate translation layer.

## Consequences

This avoids having a middle-man just re-raise the C# event. The listening controller can make a decision regarding the event.