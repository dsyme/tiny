import { HigherDimensionalObject } from '../src/dimensional_extension_module';

describe('HigherDimensionalObject Tests', () => {
    it('should correctly initialize with given dimensions', () => {
        const dimensions = 4;
        const obj = new HigherDimensionalObject(dimensions);
        expect(obj.dimensions).toBe(dimensions);
    });

    it('should correctly increase dimensions', () => {
        const obj = new HigherDimensionalObject(3);
        obj.increaseDimension();
        expect(obj.dimensions).toBe(4);
    });

    it('should return correct description after increasing dimensions', () => {
        const obj = new HigherDimensionalObject(2);
        obj.increaseDimension();
        const description = obj.describe();
        expect(description).toBe('This object exists in 3 dimensions.');
    });
});
