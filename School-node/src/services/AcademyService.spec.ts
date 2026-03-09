import { describe, it, expect, vi } from 'vitest';
import { AcademyService } from './AcademyService';
import { AcademyRepository } from '../repositories/AcademyRepository';
import { CreateAcademyDTO } from '../dtos/AcademyDTO';

describe('AcademyService', () => {
  const academyRepositoryMock = {
    create: vi.fn(),
    findByName: vi.fn(),
    findAll: vi.fn(),
    findById: vi.fn(),
    update: vi.fn(),
    delete: vi.fn(),
  } as unknown as AcademyRepository;

  const academyService = new AcademyService(academyRepositoryMock);

  it('should be able to create a new academy', async () => {
    const academyData: CreateAcademyDTO = {
      name: 'Academy Test',
      address: 'Test Street, 123',
      state: 'SP',
      city: 'São Paulo',
    };

    vi.spyOn(academyRepositoryMock, 'findByName').mockResolvedValue([]);
    vi.spyOn(academyRepositoryMock, 'create').mockResolvedValue({
      id: 1,
      ...academyData,
      createdAt: new Date(),
      updatedAt: new Date(),
    });

    const academy = await academyService.execute(academyData);

    expect(academy).toHaveProperty('id');
    expect(academy.name).toBe(academyData.name);
    expect(academyRepositoryMock.create).toHaveBeenCalledWith(academyData);
  });

  it('should not be able to create an academy with a duplicate name', async () => {
    const academyData: CreateAcademyDTO = {
      name: 'Existing Academy',
      address: 'Some address',
      state: 'RJ',
      city: 'Rio',
    };

    vi.spyOn(academyRepositoryMock, 'findByName').mockResolvedValue([{
      id: 1,
      name: 'Existing Academy',
      address: 'Old address',
      state: 'RJ',
      city: 'Rio',
      createdAt: new Date(),
      updatedAt: new Date(),
    }]);

    await expect(academyService.execute(academyData)).rejects.toThrow('Academy already exists.');
  });

  it('should be able to get all academies', async () => {
    const mockAcademies = [
      { id: 1, name: 'A1', address: 'Add1', state: 'SP', city: 'City1', createdAt: new Date(), updatedAt: new Date() },
      { id: 2, name: 'A2', address: 'Add2', state: 'RJ', city: 'City2', createdAt: new Date(), updatedAt: new Date() },
    ];

    vi.spyOn(academyRepositoryMock, 'findAll').mockResolvedValue(mockAcademies);

    const academies = await academyService.getAll();

    expect(academies).toHaveLength(2);
    expect(academies[0].name).toBe('A1');
  });

  it('should be able to get an academy by id', async () => {
    const mockAcademy = { id: 1, name: 'A1', address: 'Add1', state: 'SP', city: 'City1', createdAt: new Date(), updatedAt: new Date() };

    vi.spyOn(academyRepositoryMock, 'findById').mockResolvedValue(mockAcademy);

    const academy = await academyService.getById(1);

    expect(academy.id).toBe(1);
  });

  it('should throw error if academy is not found by id', async () => {
    vi.spyOn(academyRepositoryMock, 'findById').mockResolvedValue(null);

    await expect(academyService.getById(999)).rejects.toThrow('Academy not found');
  });

  it('should be able to delete an academy', async () => {
    const mockAcademy = { id: 1, name: 'A1', address: 'Add1', state: 'SP', city: 'City1', createdAt: new Date(), updatedAt: new Date() };
    
    vi.spyOn(academyRepositoryMock, 'findById').mockResolvedValue(mockAcademy);
    vi.spyOn(academyRepositoryMock, 'delete').mockResolvedValue(mockAcademy);

    await academyService.delete(1);

    expect(academyRepositoryMock.delete).toHaveBeenCalledWith(1);
  });
});
