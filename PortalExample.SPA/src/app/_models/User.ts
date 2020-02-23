import { Photo } from './Photo';

export interface User {
    id: number;
    username: string;
age: number;
    gender: string;
    dateOfBirth: Date;
    zodiacSign: string;
    created: Date;
    lastActive: Date;
    city: string;
    country: string;
    growth: string;
    eyeColor: string;
    hairColor: string;
    martialStatus: string;
    education: string;
    profestion: string;
    children: string;
    languages: string;
    motto: string;
    description: string;
    personality: string;
    lookingFor: string;
    inerests: string;
    freeTime: string;
    sport: string;
    movies: string;
    music: string;
    iLike: string;
    idoNotLike: string;
    makesMeLauggh: string;
    itFeelBestIn: string;
    friendeWouldDescribeMe: string;
    photos: Photo[];
    photoUrl: string;
}
