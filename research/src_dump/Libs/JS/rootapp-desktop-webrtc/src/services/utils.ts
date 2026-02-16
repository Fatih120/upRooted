/**
 * Converts a rem value to pixels
 * @param rem
 */
export const remToPx = (rem: number): number => {
  return rem * parseFloat(getComputedStyle(document.documentElement).fontSize);
};

/**
 * Converts a pixel value to a percentage of the given height
 * @param px
 * @param height
 */
export const pxToPercent = (px: number, height: number = window.innerHeight): number => {
  return px / height * 100;
};

export const percentToPx = (percent: number, height: number = window.innerHeight): number => {
  return percent / 100 * height;
};

/**
 * Flattens an object into a single level object with dot notation
 * @param obj
 * @param parentKey
 * @param separator
 */
export const flattenObject = (
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  obj: Record<string, any>,
  parentKey: string = '',
  separator: string = '.'
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
): Record<string, any> => {
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  const result: Record<string, any> = {};

  for (const [key, value] of Object.entries(obj)) {
    const newKey = parentKey ? `${parentKey}${separator}${key}` : key;

    if (value !== null && typeof value === 'object' && !Array.isArray(value)) {
      Object.assign(result, flattenObject(value, newKey, separator));
    } else {
      result[newKey] = value;
    }
  }

  return result;
};