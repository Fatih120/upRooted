/**
 * Built-in Theme Plugin -- Applies custom CSS themes to Root's UI.
 *
 * Root's entire color system is CSS variables (--rootsdk-* and --color-*).
 * This plugin overrides those variables to apply custom themes.
 *
 * Theme definitions are loaded from themes.json (shared with installer backend).
 */

import type { UprootedPlugin } from "../../types/plugin.js";
import { setCssVariables, removeCssVariable } from "../../api/native.js";
import themes from "./themes.json";

interface ThemeDef {
  name: string;
  display_name: string;
  variables: Record<string, string>;
}

// Collect all variable names across all themes for cleanup
const allVarNames = new Set<string>();
for (const theme of themes as ThemeDef[]) {
  for (const name of Object.keys(theme.variables)) {
    allVarNames.add(name);
  }
}

export default {
  name: "themes",
  description: "Built-in theme engine for Root Communications",
  version: "0.1.0",
  authors: [{ name: "Uprooted" }],

  settings: {
    theme: {
      type: "select",
      default: "default",
      description: "Which theme to apply",
      options: (themes as ThemeDef[]).map((t) => t.name),
    },
  },

  start() {
    const settings = window.__UPROOTED_SETTINGS__?.plugins?.themes?.config;
    const themeName = (settings?.theme as string) ?? "default";

    const theme = (themes as ThemeDef[]).find((t) => t.name === themeName);
    if (theme && Object.keys(theme.variables).length > 0) {
      setCssVariables(theme.variables);
    }
  },

  stop() {
    for (const name of allVarNames) {
      removeCssVariable(name);
    }
  },
} satisfies UprootedPlugin;
