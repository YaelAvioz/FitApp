export interface Login {
    username: string,
    password: string,
}

export interface Register {
    username: string,
    password: string,
    firstname: string,
    lastname: string,
    age: number,
    height: number,
    weight: number,
    gender: string,
    goal: string,
    mentor: string,
    tags: string[],
}

export interface User {
    username: string,
    firstname: string,
    lastname: string,
    age: number,
    height: number,
    weight: number,
    gender: string,
    goal: string,
    mentor: string,
}
