import {noop} from 'lodash';
import type {FormattersInitializer} from 'typesafe-i18n';
import type {Formatters, Locales} from './i18n-types';

export const initFormatters: FormattersInitializer<Locales, Formatters> = (locale: Locales) => {
  noop(locale); // remove if formatters are added

  const formatters: Formatters = {
  };

  return formatters;
};
