use std::{env, fs};


fn main() {
    let arg: Vec<_> = env::args().collect();
    let file_name: &str = &arg[1];
    let buffer: &str = &fs::read_to_string(file_name).expect("Couldn't read file");
    let index = get_start_of_packet(buffer).unwrap();
    println!("Start of packet: {index}");
    let msg_index = get_start_of_message(buffer).unwrap();
    println!("Start of message: {msg_index}");
}

fn get_start_of_packet(buffer: &str) -> Result<usize, String> {
    let buf = buffer.chars().collect::<Vec<char>>();
    let mut queue: Vec<char> = buf[0..4].to_vec();
    'iterate_queue: for index in 3..buffer.len() {
        let queue_string = queue.iter().collect::<String>();
        println!("Queue: {queue_string}");
        'check_queue: for ch in queue.to_vec() {
            if queue_string.matches(ch).count() > 1 {
                queue[0] = queue[1];
                queue[1] = queue[2];
                queue[2] = queue[3];
                queue[3] = buf[index+1];
                continue 'iterate_queue;
            }
        }
        return Ok(index + 1);
    }
    Err("Could not find packet start in buffer".to_string())
}

fn get_start_of_message(buffer: &str) -> Result<usize, String> {
    let buf = buffer.chars().collect::<Vec<char>>();
    let mut queue: Vec<char> = buf[0..14].to_vec();
    'iterate_queue: for index in 13..buffer.len() {
        let queue_string = queue.iter().collect::<String>();
        println!("Queue: {queue_string}");
        'check_queue: for ch in queue.to_vec() {
            if queue_string.matches(ch).count() > 1 {
                queue[0] = queue[1];
                queue[1] = queue[2];
                queue[2] = queue[3];
                queue[3] = queue[4];
                queue[4] = queue[5];
                queue[5] = queue[6];
                queue[6] = queue[7];
                queue[7] = queue[8];
                queue[8] = queue[9];
                queue[9] = queue[10];
                queue[10] = queue[11];
                queue[11] = queue[12];
                queue[12] = queue[13];
                queue[13] = buf[index + 1];
                continue 'iterate_queue;
            }
        }
        return Ok(index + 1);
    }
    Err("Could not find packet start in buffer".to_string())
}