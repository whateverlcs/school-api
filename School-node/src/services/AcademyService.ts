import { AcademyRepository } from '../repositories/AcademyRepository';
import { Academy } from '@prisma/client';
import { CreateAcademyDTO, UpdateAcademyDTO } from '../dtos/AcademyDTO';

export class AcademyService {
  constructor(private academyRepository: AcademyRepository) {}

  async execute(data: CreateAcademyDTO): Promise<Academy> {
    const academyAlreadyExists = await this.academyRepository.findByName(data.name);

    if (academyAlreadyExists.length > 0) {
      throw new Error('Academy already exists.');
    }

    const academy = await this.academyRepository.create(data);

    return academy;
  }

  async getById(id: number): Promise<Academy> {
    const academy = await this.academyRepository.findById(id);

    if (!academy) {
      throw new Error('Academy not found');
    }

    return academy;
  }

  async getByName(name: string): Promise<Academy[]> {
    return this.academyRepository.findByName(name);
  }

  async getByState(state: string): Promise<Academy[]> {
    return this.academyRepository.findByState(state);
  }

  async getByCity(city: string): Promise<Academy[]> {
    return this.academyRepository.findByCity(city);
  }

  async getAll(): Promise<Academy[]> {
    return this.academyRepository.findAll();
  }

  async update(id: number, data: UpdateAcademyDTO): Promise<Academy> {
    return this.academyRepository.update(id, data);
  }

  async delete(id: number): Promise<void> {
    await this.getById(id);
    await this.academyRepository.delete(id);
  }
}
