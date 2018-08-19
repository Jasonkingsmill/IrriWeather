
export class ScheduleApiModel {
    id: string;
    name: string;
    description: string;
    channel: number;
    isEnabled: boolean;
    isStarted: boolean;
}

export default ScheduleApiModel;