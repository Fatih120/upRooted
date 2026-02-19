import type { UprootedPlugin } from "../../types/plugin.js";

const silentTypingPlugin: UprootedPlugin = {
  name: "silent-typing",
  description: "Hide that you are typing",
  version: "0.2.0",
  authors: [{ name: "Kurumi Nanase" }],
  // Blocking is handled by the C# SilentTypingEngine hook.
  // SetTypingIndicator gRPC calls originate from Root's .NET layer, not from
  // DotNetBrowser, so JS fetch/XHR interception has no effect here.
  start() {},
  stop() {},
};

export default silentTypingPlugin;
