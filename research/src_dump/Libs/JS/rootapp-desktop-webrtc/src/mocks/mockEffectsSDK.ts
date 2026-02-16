export const MOCK_EFFECTS_SDK_CONFIG = {
  preset: 'balanced',
  sample_rate: 48000,
  processingChunk: 64,
  sdk_url: 'https://effectssdk.ai/sdk/audio/',
  processorType: 'worklet',
  wasmPaths: {
    'ort-wasm.wasm': 'https://effectssdk.ai/sdk/audio/dev/3.2.2/ort-wasm.wasm',
    'ort-wasm-simd.wasm': 'https://effectssdk.ai/sdk/audio/dev/3.2.2/ort-wasm-simd.wasm'
  }
};