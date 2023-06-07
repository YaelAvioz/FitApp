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

//TODO: chnage in the server to weight and date
export interface Weight{
item1: number,
item2: Date,
}

export interface User {
    username: string,
    firstname: string,
    lastname: string,
    age: number,
    height: number,
    weight: Weight[],
    bmi: number,
    gender: string,
    goal: string,
    mentor: string,
}

export interface RegisterResponse {
    age: number,
    bmi: number,
    firstMsg: string,
    firstname: string,
    gender: string,
    goal: string,
    height: number,
    lastname: string,
    mentor: string,
    tags: string[],
    token: string,
    username: string
}

