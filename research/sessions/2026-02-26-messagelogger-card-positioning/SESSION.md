# Dump Session: MessageLogger Card Positioning
Date: 2026-02-26
Task: FindMessageGridInContainer returns null — container structure investigation
Hook files: hook/MessageLogger.cs, hook/MessageStore.cs

## Types Dumped
| File | Assembly | Full Type | Lines | Why |
|------|----------|-----------|-------|-----|
| MessageView.cs | RootApp.Client.Avalonia | ...UI.Messages.MessageView | 3622 | Primary message view (text messages): visual tree structure |
| MessageViewModel.cs | RootApp.Client.Avalonia | ...UI.Messages.MessageViewModel | 784 | Message data model, sub-collections (links, media, files) |
| RootMarkdownTextBlock.cs | RootApp.Client.Avalonia | ...Controls.RootMarkdownTextBlock | 89 | ContentControl searched by FindMessageGridInContainer |
| RootLinkButton.cs | RootApp.Client.Avalonia | ...Controls.RootLinkButton | 28 | Fallback control searched by FindMessageGridInContainer |
| RootMessageItemsControl.cs | RootApp.Client.Avalonia | ...Controls.RootMessageItemsControl | 377 | Chat message list (ItemsControl subclass) |
| TextChannelContentView.cs | RootApp.Client.Avalonia | ...Community.Content.TextChannelContentView | 1624 | View hosting the message list |
| ImageMessageView.cs | RootApp.Client.Avalonia | ...UI.Messages.ImageMessageView | 481 | Image message view: NO RootMarkdownTextBlock |
| GifMessageView.cs | RootApp.Client.Avalonia | ...UI.Messages.GifMessageView | 526 | GIF message view: NO RootMarkdownTextBlock |
| CommunityMessageView.cs | RootApp.Client.Avalonia | ...UI.Messages.CommunityMessageView | 449 | Community system message: NO RootMarkdownTextBlock |
| DeleteMessageView.cs | RootApp.Client.Avalonia | ...UI.Messages.DeleteMessageView | 125 | Delete system message: NO RootMarkdownTextBlock |
| LinkMessageView.cs | RootApp.Client.Avalonia | ...UI.Messages.LinkMessageView | 547 | Link message sub-view (lives inside MessageView row 2) |

## Key Findings

### Root Cause: Mixed item types in the message list

Root's message list (`RootMessageItemsControl`) contains **12 different ViewModel types** via ViewFactory, each mapped to a distinct View:

| ViewModel | View | Has RootMarkdownTextBlock? |
|-----------|------|:---:|
| MessageViewModel | MessageView | YES |
| ImageMessageViewModel | ImageMessageView | NO |
| GifMessageViewModel | GifMessageView | NO |
| FileMessageViewModel | FileMessageView | NO |
| VideoMessageViewModel | VideoMessageView | NO |
| LinkMessageViewModel | LinkMessageView | NO (sub-view of MessageView) |
| CommunityMessageViewModel | CommunityMessageView | NO |
| ChannelStartMessageViewModel | ChannelStartMessageView | NO |
| DeleteMessageViewModel | DeleteMessageView | NO |
| DirectMessageStartMessageViewModel | ... | NO |
| PendingFileMessageViewModel | ... | NO |
| PendingMediaMessageViewModel | ... | NO |

`FindMessageGridInContainer` only searches for `RootMarkdownTextBlock` or `RootLinkButton`, which exist **only in MessageView**. When the nearest visible message before a deleted message is an image, GIF, file, video, or system message, the method returns null and the deleted-message card is never injected.

### MessageView visual tree (confirmed working for text messages)

```
ContentPresenter (VSP child)
  └── MessageView (UserControl, ISelectableMessage)
        └── Grid (grid5) — 3 rows: Auto, Auto, Star
              ├── Row 0: Grid — "New Messages" divider
              ├── Row 1: Grid — Date divider
              └── Row 2: Border ("MessageBackgroundBorder")
                    └── Panel
                          ├── Border ("MessageBackgroundHighlightBorder")
                          └── Grid (grid17) — cols: 34px, 16px, Star; rows: Auto, Star
                                ├── Row 0: ContentControl (reply container)
                                ├── Row 1, Col 0: Button (avatar)
                                └── Row 1, Col 2: Grid (grid21) — THE TARGET GRID
                                      ├── Cols: Auto, Auto, 8px, Star
                                      ├── 8 rows: Auto, Star, Auto×6
                                      ├── Row 0: RootLinkButton (username), badges, timestamp
                                      ├── Row 1: RootMarkdownTextBlock (message text)
                                      ├── Row 2: ItemsControl (Links)
                                      ├── Row 3: ItemsControl (Media — Image/GIF/Video sub-views)
                                      ├── Row 4: ItemsControl (Files)
                                      ├── Row 5: StackPanel (Edited/Pinned indicators)
                                      └── Rows 6-7: Reactions, etc.
```

For text messages, `GetParent(RootMarkdownTextBlock)` → grid21 (Grid) → works correctly.

### How property resolution succeeds for non-MessageView types

`TryResolvePropertyAccessors` uses a bridge pattern: if the DataContext type doesn't have direct `Id`/`Content` properties, it checks nested properties. For `ImageMessageViewModel`, a property like `Message` bridges to the underlying `Message` model which has `Id`, `MessageContent`, etc. So `ReadMessageId(dc)` succeeds for ALL message types, meaning `visibleMessages` includes image/GIF/file/video containers.

### ImageMessageView structure (example of failing type)

```
ImageMessageView (UserControl)
  └── Panel
        ├── Button → RootImageLoader (the image)
        └── Grid (hover toolbar: copy/save/open actions)
```

No Grid suitable for row injection. No RootMarkdownTextBlock. No RootLinkButton.
