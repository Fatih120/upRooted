/**
 * CLI: Install Uprooted into Root's profile directory.
 *
 * Usage: pnpm install-root
 */

import path from "node:path";
import { install } from "../src/core/patcher.js";

const distDir = path.resolve(import.meta.dirname, "..", "dist");
install(distDir);
