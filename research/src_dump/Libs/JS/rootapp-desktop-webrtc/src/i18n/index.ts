import {detectLocale, i18nObject} from './i18n-util';
import {loadLocale} from './i18n-util.sync';

const locale = detectLocale();
loadLocale(locale);

export const Lang = i18nObject(locale);