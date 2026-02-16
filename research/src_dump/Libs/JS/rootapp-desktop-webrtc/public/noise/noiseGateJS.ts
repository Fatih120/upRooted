export const noiseGateJS = `
class NoiseGateProcessor extends AudioWorkletProcessor {
	incrementor = 0.0;	
	decrementor = 0.0;
	fullon = 0.2;				
	lastState = false;
	updateRate = 1.0/5.0;
	lastUpdate;
	current = [ 0, 0, 0, 0, 0, 0, 0, 0 ];
	running = true;
	pushToTalk;

  static get parameterDescriptors() {
    return [
      { name: 'threshold', defaultValue: 0.025 },
    ];
  }

  constructor(...args) {
    super(...args);
		this.incrementor = 50.0 / (sampleRate / 128.0);
		this.decrementor = this.incrementor / 30.0;
		this.lastUpdate = -1;
		this.port.onmessage = (e) => {
			if('pushToTalk' in e.data) {
				this.pushToTalk = e.data.pushToTalk;
				if(this.pushToTalk === false) {
					this.port.postMessage({ talking: false });
					this.lastState = false;
					this.current[0] = this.current[1] = this.current[2] = this.current[3] = 0;
				}
			}
			else { 
				this.running = false;
			}
		};
  }

  process(inputs, outputs, parameters) {
    const input = inputs[0];
    const output = outputs[0];
		const threshold = parameters.threshold[0];

		let totalCurrent = 0;
    for (let channel = 0; channel < input.length; channel++) {
      const inputChannel = input[channel];
			if (!inputChannel) continue;

      const outputChannel = output[channel];
			
			let multiplier = 0.0;
			let sum = 0;
			let rms = 0;
			if(this.pushToTalk === undefined) { 
				for (let i = 0; i < inputChannel.length; i++) {
					sum += inputChannel[i] * inputChannel[i];
				}
				rms = Math.sqrt(sum / inputChannel.length);
			}
			else if(this.pushToTalk) { rms = 1; }
			else { rms = 0; }

			if(rms > threshold) { 
				this.current[channel] = Math.min(this.current[channel] + this.incrementor, 1.0);
			}
			else {
				this.current[channel] = Math.max(this.current[channel] - this.decrementor, 0.0);
			}
			totalCurrent += this.current[channel];

			multiplier = Math.min(this.current[channel] / this.fullon, 1.0);

			for (let i = 0; i < inputChannel.length; i++) {
				outputChannel[i] = inputChannel[i] * multiplier;
			}
    }

		if(this.lastUpdate + this.updateRate < currentTime) { 
			if(totalCurrent >= 0.0001 && !this.lastState) { 
				this.lastState = true;
				this.port.postMessage({ talking: true });
			}
			else if(totalCurrent < 0.0001 && this.lastState) { 
				this.lastState = false;
				this.port.postMessage({ talking: false });
			}
			this.lastUpdate = currentTime;
		}

    return this.running;
  }
}

registerProcessor('noise-gate-worklet', NoiseGateProcessor);
`;